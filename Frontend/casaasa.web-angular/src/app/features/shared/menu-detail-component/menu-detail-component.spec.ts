import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuDetailComponent } from './menu-detail-component';

describe('MenuDetailComponent', () => {
  let component: MenuDetailComponent;
  let fixture: ComponentFixture<MenuDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
