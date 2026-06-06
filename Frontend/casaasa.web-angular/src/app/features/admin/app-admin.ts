import { Component } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { DashboardAdmin } from "./dashboard.admin/dashboard.admin";

@Component({
  selector: 'app-admin',
  imports: [RouterOutlet, DashboardAdmin],
  templateUrl: './app-admin.html',
  styleUrl: './app-admin.scss',
})
export class AppAdmin {}
