import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import {
  forEach as _forEach,
  includes as _includes,
  map as _map,
} from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import {
  RoleServiceProxy,
  GetRoleForEditOutput,
  RoleDto,
  PermissionDto,
  RoleEditDto,
  FlatPermissionDto,
} from "@shared/service-proxies/service-proxies";
import { TreeNode } from "primeng/api";
import { ArrayToTreeConverterService } from "shared/ArrayToTreeConverterService";
import * as _ from "lodash";
import { DynamicDialogConfig, DynamicDialogRef } from "primeng/dynamicdialog";

@Component({
  templateUrl: "edit-role-dialog.component.html",
})
export class EditRoleDialogComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  id: number;
  role = new RoleEditDto();
  permissions: FlatPermissionDto[];
  grantedPermissionNames: string[];
  checkedPermissionsMap: { [key: string]: boolean } = {};

  @Output() onSave = new EventEmitter<any>();

  treeData: any[] = [];
  selectedPermissions: TreeNode[] = [];

  constructor(
    injector: Injector,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private _arrayToTreeConverterService: ArrayToTreeConverterService,
    private _roleService: RoleServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    // this._roleService
    //   .getRoleForEdit(this.id)
    //   .subscribe((result: GetRoleForEditOutput) => {
    //     this.role = result.role;
    //     this.permissions = result.permissions;
    //     this.grantedPermissionNames = result.grantedPermissionNames;
    //     this.setInitialPermissionsStatus();
    //     this.cd.detectChanges();
    //   });
    this.id =
      this.config?.data && this.config.data["id"]
        ? this.config.data["id"]
        : undefined;
    if (this.id) {
      this._roleService
        .getRoleForEdit(this.id)
        .subscribe((result: GetRoleForEditOutput) => {
          this.setTreeData(result.permissions);
          this.role.init(result.role);
          this.grantedPermissionNames = result.grantedPermissionNames;
          this.setSelectedNodes(this.grantedPermissionNames);
        });
    } else {
      this._roleService.getAllPermissions().subscribe((result) => {
        this.permissions = result.items;
        this.setTreeData(result.items);
        this.grantedPermissionNames = [];
        this.setSelectedNodes(this.grantedPermissionNames);
      });
    }
  }

  handleNodeToggle(event: any): void {
    const node = event.node;
    this.pushPartialSelectedPermissionsToSelectedPermissions(node);
  }

  pushPartialSelectedPermissionsToSelectedPermissions(node: any) {
    let partialPermission =
      node && node.parent && node.parent.partialSelected
        ? node.parent
        : undefined;
    if (partialPermission) {
      partialPermission.partialSelected = false;
      this.pushPermission(partialPermission);
    }
    node &&
      this.pushPartialSelectedPermissionsToSelectedPermissions(node.parent);
  }

  close(data?: any) {
    this.ref.close(data);
  }

  pushPermission(permission: any) {
    let isAlreadyExist = this.selectedPermissions.findIndex(
      (a) => a.data.name == permission.data.name
    );
    isAlreadyExist < 0 && this.selectedPermissions.push(permission);
  }

  setInitialPermissionsStatus(): void {
    _map(this.permissions, (item) => {
      this.checkedPermissionsMap[item.name] = this.isPermissionChecked(
        item.name
      );
    });
  }

  isPermissionChecked(permissionName: string): boolean {
    return _includes(this.grantedPermissionNames, permissionName);
  }

  onPermissionChange(permission: FlatPermissionDto, $event) {
    this.checkedPermissionsMap[permission.name] = $event.target.checked;
  }

  getCheckedPermissions(): string[] {
    const permissions: string[] = [];
    _forEach(this.checkedPermissionsMap, function (value, key) {
      if (value) {
        permissions.push(key);
      }
    });
    return permissions;
  }

  save(): void {
    this.saving = true;

    const role = new RoleDto();
    role.init(this.role);
    role.grantedPermissions = this.getCheckedPermissions();

    this._roleService.update(role).subscribe(
      () => {
        this.notify.info(this.l("SavedSuccessfully"));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }

  setTreeData(permissions: FlatPermissionDto[]) {
    this.treeData = this._arrayToTreeConverterService.createTree(
      permissions,
      "parentName",
      "name",
      null,
      "children",
      [
        {
          target: "label",
          source: "displayName",
        },
        {
          target: "expandedIcon",
          value: "fa fa-folder-open m--font-warning",
        },
        {
          target: "collapsedIcon",
          value: "fa fa-folder m--font-warning",
        },
        {
          target: "expanded",
          value: false,
        },
      ]
    );
  }
  setSelectedNodes(grantedPermissionNames: string[]) {
    _.forEach(grantedPermissionNames, (permission) => {
      let item = this.findNode(this.treeData, { data: { name: permission } });
      if (item) {
        this.selectedPermissions.push(item);
      }
    });
  }

  findNode(data, selector): any {
    let nodes = _.filter(data, selector);
    if (nodes && nodes.length === 1) {
      return nodes[0];
    }

    let foundNode = null;

    _.forEach(data, (d) => {
      if (!foundNode) {
        foundNode = this.findNode(d.children, selector);
      }
    });

    return foundNode;
  }
}
