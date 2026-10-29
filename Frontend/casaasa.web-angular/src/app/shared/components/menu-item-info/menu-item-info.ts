import { Component, DestroyRef, forwardRef, 
  inject, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { MenuApiService } from '../../services/features/menu-api.service';
import { ControlValueAccessor, FormBuilder, NG_VALUE_ACCESSOR,
  ReactiveFormsModule, Validators } from '@angular/forms';
import { MenuDetailModel } from '../../models/menu/menu-detail.model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MenuCategoryModel } from '../../models/menu/menu-category.model';

@Component({
  selector: 'menu-item-info',
  imports: [ReactiveFormsModule],
  templateUrl: './menu-item-info.html',
  styleUrl: './menu-item-info.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MenuItemInfo),
      multi: true
    }
  ]
})
export class MenuItemInfo implements OnInit, ControlValueAccessor {
  private _menuItemFormBuilder = inject(FormBuilder);
  private _menuApiService = inject(MenuApiService);
  private _destroyRef = inject(DestroyRef);
  private _cdRef = inject(ChangeDetectorRef);

  @Input() isReadonly = false;

  private onMenuItemChange = (_: MenuDetailModel | null) => {};
  categories: MenuCategoryModel[] = [];

  readonly menuItemForm = this._menuItemFormBuilder.group({
    id: [0],
    name: ['', [Validators.required]],
    description: ['', [Validators.required]],
    categoryId: [0, [Validators.required, Validators.min(1)]],
    categoryName: ['', [Validators.required]],
    price: [0, [Validators.required, Validators.min(1)]],
    //imageUrl: ['']
  });

  constructor() {
    this.menuItemForm.valueChanges
      .pipe(takeUntilDestroyed(this._destroyRef))
      .subscribe(value => {
        if (this.menuItemForm.valid) {
          this.onMenuItemChange(value as MenuDetailModel);
        } 
        else {
          this.onMenuItemChange(null);
        }
      });
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  private loadCategories(): void {
    this._menuApiService.getMenuCategories()
      .pipe(takeUntilDestroyed(this._destroyRef))
      .subscribe({
        next: (result: MenuCategoryModel[]) => {
          this.categories = result;
          this._cdRef.detectChanges();

          console.log("Loaded categories:", this.categories);
        },
        error: (err) => {
          console.error("Failed to load categories:", err);
        }
      });
  }

  writeValue(value: MenuDetailModel): void {
    if (!value) {
      this.menuItemForm.reset();
      return;
    }

    this.menuItemForm.patchValue(value, { emitEvent: false });
  }

  registerOnChange(fn: (value: MenuDetailModel | null) => void): void { 
    this.onMenuItemChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onMenuItemChange = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.menuItemForm.disable();
    } 
    else {
      this.menuItemForm.enable();
    }
  }  
}