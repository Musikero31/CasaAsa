import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuDetail } from './menu-detail';

describe('MenuDetail', () => {
  let component: MenuDetail;
  let fixture: ComponentFixture<MenuDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuDetail],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuDetail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
