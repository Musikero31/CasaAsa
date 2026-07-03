import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppAdmin } from './app-admin';

describe('AppAdmin', () => {
  let component: AppAdmin;
  let fixture: ComponentFixture<AppAdmin>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppAdmin],
    }).compileComponents();

    fixture = TestBed.createComponent(AppAdmin);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
