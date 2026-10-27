import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealOfTheDayCustomer } from './meal-of-the-day.customer';

describe('MealOfTheDayCustomer', () => {
  let component: MealOfTheDayCustomer;
  let fixture: ComponentFixture<MealOfTheDayCustomer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MealOfTheDayCustomer],
    }).compileComponents();

    fixture = TestBed.createComponent(MealOfTheDayCustomer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
