import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { CreateUserDialogComponent } from './create-user/create-user-dialog.component';
import { EditUserDialogComponent } from './edit-user/edit-user-dialog.component';
import { ResetPasswordDialogComponent } from './reset-password/reset-password.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';

@NgModule({
    declarations: [UsersComponent, ResetPasswordDialogComponent, EditUserDialogComponent, CreateUserDialogComponent, ChangePasswordComponent],
    imports: [SharedModule, UsersRoutingModule, PaginatorModule, TableModule, CommonModule, MenuModule, ButtonModule],
})
export class UsersModule {}