// import {
//   Component,
//   Injector,
//   OnInit,
//   EventEmitter,
//   Output,
//   ChangeDetectorRef
// } from '@angular/core';
// import { BsModalRef } from 'ngx-bootstrap/modal';
// import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
// import { AppComponentBase } from '@shared/app-component-base';
// import {
//   UserServiceProxy,
//   UserDto,
//   RoleDto
// } from '@shared/service-proxies/service-proxies';

// @Component({
//   templateUrl: './edit-user-dialog.component.html'
// })
// export class EditUserDialogComponent extends AppComponentBase
//   implements OnInit {
//   saving = false;
//   user = new UserDto();
//   roles: RoleDto[] = [];
//   checkedRolesMap: { [key: string]: boolean } = {};
//   id: number;

//   selectedRole: any = null;

//   @Output() onSave = new EventEmitter<any>();

//   constructor(
//     injector: Injector,
//     public _userService: UserServiceProxy,
//     public bsModalRef: BsModalRef,
//     private cd: ChangeDetectorRef
//   ) {
//     super(injector);
//   }

//   ngOnInit(): void {
//     this._userService.get(this.id).subscribe((result) => {
//       this.user = result;

//       this._userService.getRoles().subscribe((result2) => {
//         this.roles = result2.items;
//         this.setInitialRolesStatus();
//         this.cd.detectChanges();
//       });
//     });
//   }

//   setInitialRolesStatus(): void {
//     _map(this.roles, (item) => {
//       this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(
//         item.normalizedName
//       );
//     });
//   }

//   isRoleChecked(normalizedName: string): boolean {
//    return _includes(this.user.roleNames, normalizedName);
//    }
  
//    onRoleChange(role: RoleDto, $event) {
//      this.checkedRolesMap[role.normalizedName] = $event.target.checked;
//    }

//   onRoleChange(selectedRole: any): void {
//     // Update the selected role
//     this.selectedRole = selectedRole;
//   }
  

//   getCheckedRoles(): string[] {
//     const roles: string[] = [];
//     _forEach(this.checkedRolesMap, function (value, key) {
//       if (value) {
//         roles.push(key);
//       }
//     });
//     return roles;
//   }

//    save(): void {
//      this.saving = true;

//      this.user.roleNames = this.getCheckedRoles();

//      this._userService.update(this.user).subscribe(
//        () => {
//          this.notify.info(this.l('SavedSuccessfully'));
//          this.bsModalRef.hide();
//          this.onSave.emit();
//        },
//        () => {
//          this.saving = false;
//        }
//      );
//    }
 
// }

import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  UserDto,
  RoleDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './edit-user-dialog.component.html'
})
export class EditUserDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  user = new UserDto();
  roles: RoleDto[] = [];
  checkedRolesMap: { [key: string]: boolean } = {};
  id: number;

  selectedRole: any = null; // Property to hold the selected role

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._userService.get(this.id).subscribe((result) => {
      this.user = result;

      // Get the available roles and set initial status
      this._userService.getRoles().subscribe((result2) => {
        this.roles = result2.items;
        this.setInitialRolesStatus();
        this.cd.detectChanges();
      });
    });
  }

  // Set initial selected role based on user's current roles
  setInitialRolesStatus(): void {
    // If the user has a role, assign it to selectedRole
    if (this.user.roleNames && this.user.roleNames.length > 0) {
      this.selectedRole = this.roles.find(
        (role) => role.normalizedName === this.user.roleNames[0]
      );
    }
    // Initialize the checked status for each role
    _map(this.roles, (item) => {
      this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(item.normalizedName);
    });
  }

  // Check if a role is selected (radio button logic)
  isRoleChecked(normalizedName: string): boolean {
    return this.selectedRole?.normalizedName === normalizedName;
  }

  // Handle role selection change (radio button logic)
  onRoleChange(selectedRole: any): void {
    this.selectedRole = selectedRole;
    console.log('Selected role:', this.selectedRole); // Debugging statement
  }

  // Save method
  save(): void {
    this.saving = true;

    // Assign the selected role to user.roleNames
    if (this.selectedRole) {
      this.user.roleNames = [this.selectedRole.normalizedName]; // Save selected role
    } else {
      this.user.roleNames = []; // If no role is selected, clear roleNames
    }

    // Update user with selected role
    this._userService.update(this.user).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
