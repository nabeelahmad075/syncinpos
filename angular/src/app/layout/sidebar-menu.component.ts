import { Component, Injector, OnInit } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  Router,
  RouterEvent,
  NavigationEnd,
  PRIMARY_OUTLET,
} from "@angular/router";
import { BehaviorSubject } from "rxjs";
import { filter } from "rxjs/operators";
import { MenuItem } from "@shared/layout/menu-item";

@Component({
  selector: "sidebar-menu",
  templateUrl: "./sidebar-menu.component.html",
})
export class SidebarMenuComponent extends AppComponentBase implements OnInit {
  menuItems: MenuItem[];
  menuItemsMap: { [key: number]: MenuItem } = {};
  activatedMenuItems: MenuItem[] = [];
  routerEvents: BehaviorSubject<RouterEvent> = new BehaviorSubject(undefined);
  homeRoute = "/app/home";

  constructor(injector: Injector, private router: Router) {
    super(injector);
  }

  ngOnInit(): void {
    this.menuItems = this.getMenuItems();
    this.patchMenuItems(this.menuItems);
    this.deactivateMenuItems(this.menuItems);

    this.router.events.subscribe((event: NavigationEnd) => {
      const currentUrl = event.url !== "/" ? event.url : this.homeRoute;
      const primaryUrlSegmentGroup =
        this.router.parseUrl(currentUrl).root.children[PRIMARY_OUTLET];
      if (primaryUrlSegmentGroup) {
        this.activateMenuItems("/" + primaryUrlSegmentGroup.toString());
      }
    });
  }

  getMenuItems(): MenuItem[] {
    return [
      new MenuItem(this.l("Setup"), "", "fa-solid fa-gear", "", [
        new MenuItem("Configuration", "", "fa-solid fa-sliders", "", [
          new MenuItem(
            "Location",
            "/app/locationhistory",
            "fa-solid fa-map-pin"
          ),
          new MenuItem(
            "Add Tables",
            "/app/tables",
            "fa-solid fa-dumpster"
          ),
        ]),
        new MenuItem("HR Management", "", "fa-solid fa-user", "", [
          new MenuItem(
            "Add Employee",
            "/app/employeehistory",
            "fa-solid fa-user-plus"
          ),
        ]),
        new MenuItem("Menu Operations", "", "fa-solid fa-utensils", "", [
          new MenuItem("Section", "/app/section-history", "fa-solid fa-puzzle-piece"),
          new MenuItem(
            "Category",
            "/app/categoryhistory",
            "fa-solid fa-list"
          ),
          new MenuItem(
            "Item Defination",
            "/app/itemdefinitionhistory",
            "fa-solid fa-drumstick-bite"
          ),
        ]),
      ]),
      new MenuItem(this.l("Accounts"), "", "fa-solid fa-chart-line", "", [
        new MenuItem(
          "Chart of Accounts",
          "/app/coa-detail",
          "fa-solid fa-chart-gantt"
        ),
        new MenuItem(
          "Voucher Entry",
          "/app/voucher-entry",
          "fa-solid fa-ticket"
        ),
      ]),
      new MenuItem(this.l("HomePage"), "/app/home", "fas fa-home"),
      new MenuItem(
        this.l("Roles"),
        "/app/roles",
        "fas fa-theater-masks",
        "Pages.Roles"
      ),
      new MenuItem(
        this.l("Tenants"),
        "/app/tenants",
        "fas fa-building",
        "Pages.Tenants"
      ),
      new MenuItem(
        this.l("Users"),
        "/app/users",
        "fas fa-users",
        "Pages.Users"
      ),
    ];
  }

  patchMenuItems(items: MenuItem[], parentId?: number): void {
    items.forEach((item: MenuItem, index: number) => {
      item.id = parentId ? Number(parentId + "" + (index + 1)) : index + 1;
      if (parentId) {
        item.parentId = parentId;
      }
      if (parentId || item.children) {
        this.menuItemsMap[item.id] = item;
      }
      if (item.children) {
        this.patchMenuItems(item.children, item.id);
      }
    });
  }

  // activateMenuItems(url: string): void {
  //   this.deactivateMenuItems(this.menuItems);
  //   this.activatedMenuItems = [];
  //   const foundedItems = this.findMenuItemsByUrl(url, this.menuItems);
  //   foundedItems.forEach((item) => {
  //     this.activateMenuItem(item);
  //   });
  // }

  // deactivateMenuItems(items: MenuItem[]): void {
  //   items.forEach((item: MenuItem) => {
  //     item.isActive = false;
  //     item.isCollapsed = true;
  //     if (item.children) {
  //       this.deactivateMenuItems(item.children);
  //     }
  //   });
  // }

  findMenuItemsByUrl(
    url: string,
    items: MenuItem[],
    foundedItems: MenuItem[] = []
  ): MenuItem[] {
    items.forEach((item: MenuItem) => {
      if (item.route === url) {
        foundedItems.push(item);
      } else if (item.children) {
        this.findMenuItemsByUrl(url, item.children, foundedItems);
      }
    });
    return foundedItems;
  }

  // activateMenuItem(item: MenuItem): void {
  //   item.isActive = true;
  //   if (item.children) {
  //     item.isCollapsed = true;
  //   }
  //   this.activatedMenuItems.push(item);
  //   if (item.parentId) {
  //     this.activateMenuItem(this.menuItemsMap[item.parentId]);
  //   }
  // }

//   activateMenuItem(item: MenuItem): void {
//     item.isActive = true;

//     // Expand the menu if it has children
//     if (item.children) {
//         item.isCollapsed = false;
//     }

//     // Activate parent menu if it exists
//     if (item.parentId) {
//         const parentItem = this.menuItemsMap[item.parentId];
//         if (parentItem) {
//             this.activateMenuItem(parentItem); // Recursively activate parent
//         }
//     }
// }


  isMenuItemVisible(item: MenuItem): boolean {
    if (!item.permissionName) {
      return true;
    }
    return this.permission.isGranted(item.permissionName);
  }

// toggleMenuItem(item: MenuItem): void {
//   // If the item is already expanded, toggle it closed
//   item.isCollapsed = !item.isCollapsed;

//   // If expanding, collapse all other top-level menu items
//   if (!item.isCollapsed) {
//       this.menuItems.forEach((menu) => {
//           if (menu !== item) {
//               menu.isCollapsed = true;
//           }
//       });
//   }
// }

toggleMenuItem(item: MenuItem): void {
  // Toggle the current item's collapsed state
  item.isCollapsed = !item.isCollapsed;

  // If expanding this item, ensure its parent remains open
  if (!item.isCollapsed && item.parentId) {
    const parentItem = this.menuItemsMap[item.parentId];
    if (parentItem) {
      parentItem.isCollapsed = false; // Expand the parent
    }
  }
}

activateMenuItems(url: string): void {
  // Deactivate all menu items
  this.deactivateMenuItems(this.menuItems);

  // Find all menu items matching the current URL
  const matchedItems = this.findMenuItemsByUrl(url, this.menuItems);

  // Activate the matched items and their parents
  matchedItems.forEach((item) => {
    this.activateMenuItemWithParents(item);
  });

  // Keep top-level menus open if they have active children
  this.menuItems.forEach((menu) => {
    if (menu.children?.some((child) => child.isActive)) {
      menu.isCollapsed = false; // Expand if it contains an active child
    }
  });
}


activateMenuItemWithParents(item: MenuItem): void {
  item.isActive = true; // Mark the item as active
  item.isCollapsed = false; // Expand the item if it has children

  // Recursively activate its parent, if any
  if (item.parentId) {
    const parentItem = this.menuItemsMap[item.parentId];
    if (parentItem) {
      parentItem.isCollapsed = false; // Ensure parent is expanded
      this.activateMenuItemWithParents(parentItem); // Activate the parent
    }
  }
}


deactivateMenuItems(items: MenuItem[]): void {
  items.forEach((item: MenuItem) => {
    item.isActive = false; // Deactivate the item
    item.isCollapsed = true; // Collapse all items initially
    if (item.children) {
      this.deactivateMenuItems(item.children); // Recursively deactivate children
    }
  });
}


}
