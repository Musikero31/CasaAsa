import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuItemInfo } from './menu-item-info';

describe('MenuItemInfo', () => {
  let component: MenuItemInfo;
  let fixture: ComponentFixture<MenuItemInfo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuItemInfo],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuItemInfo);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
