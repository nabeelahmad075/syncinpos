import { Component } from "@angular/core";
import { PrimeNG } from "primeng/config";
import Lara from "@primeng/themes/lara";

@Component({
  selector: "app-root",
  template: `<router-outlet></router-outlet>`,
})
export class RootComponent {
  constructor(private primeng: PrimeNG) {
    this.primeng.theme.set({
      preset: Lara,
      options: {
        cssLayer: {
          name: "primeng",
          order: "tailwind-base, primeng, tailwind-utilities",
        },
      },
    });
  }
}
