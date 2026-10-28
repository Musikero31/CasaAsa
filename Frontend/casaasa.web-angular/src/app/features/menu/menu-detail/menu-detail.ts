import { Component, inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { OnInit } from '@angular/core';
import { MenuItemInfo } from '../../../shared/components/menu-item-info/menu-item-info';

@Component({
  selector: 'casa-menu-detail',
  imports: [MenuItemInfo],
  templateUrl: './menu-detail.html',
  styleUrl: './menu-detail.scss',
})
export class MenuDetail implements OnInit {
  private _menuForm = inject(FormBuilder);

  menuForm = this._menuForm.group({})

  constructor() {}

  ngOnInit(): void {

  }
  
  onSave() {}
  onCancel() {}

  
}
