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
      new MenuItem(this.l("HomePage"), "/app/home", "fas fa-home text-md"),
      new MenuItem(
        this.l("Setup"),
        "/app/setup-menu",
        "fa-solid fa-gear text-md"
      ),
      new MenuItem(
        this.l("Sales"),
        "/app/sales-menu",
        "fa-solid fa-chart-pie text-md"
      ),
      new MenuItem(
        this.l("Accounts"),
        "/app/accounts-menu",
        "fa-solid fa-chart-line text-md"
      ),
      new MenuItem(
        this.l("Reports"),
        "/app/reports",
        "fa-solid fa-file-invoice text-md"
      ),
      // new MenuItem(
      //   this.l("Roles"),
      //   "/app/roles",
      //   "fas fa-theater-masks text-md",
      //   "Pages.Roles"
      // ),
      new MenuItem(
        this.l("Tenants"),
        "/app/tenants",
        "fas fa-building text-md",
        "Pages.Tenants"
      ),
      new MenuItem(
        this.l("Utility"),
        "/app/utility",
        "fa-solid fa-download text-md",
        "Pages.Utility"
      ),
      // new MenuItem(
      //   this.l("Users"),
      //   "/app/users",
      //   "fas fa-users text-md",
      //   "Pages.Users"
      // ),
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

  isMenuItemVisible(item: MenuItem): boolean {
    if (!item.permissionName) {
      return true;
    }
    return this.permission.isGranted(item.permissionName);
  }

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
