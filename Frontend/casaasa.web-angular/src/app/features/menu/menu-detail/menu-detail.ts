import { Component, inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'casa-menu-detail',
  imports: [],
  templateUrl: './menu-detail.html',
  styleUrl: './menu-detail.scss',
})
export class MenuDetail {
  private _menuForm = inject(FormBuilder);

  menuForm = this._menuForm.group({})

  onSave() {}
  onCancel() {}
}
