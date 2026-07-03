import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppCustomer } from './app-customer';

describe('AppCustomer', () => {
  let component: AppCustomer;
  let fixture: ComponentFixture<AppCustomer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppCustomer],
    }).compileComponents();

    fixture = TestBed.createComponent(AppCustomer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
