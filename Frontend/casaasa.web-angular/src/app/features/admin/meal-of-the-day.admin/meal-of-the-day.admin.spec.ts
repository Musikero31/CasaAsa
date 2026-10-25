import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealOfTheDayAdmin } from './meal-of-the-day.admin';

describe('MealOfTheDayAdmin', () => {
  let component: MealOfTheDayAdmin;
  let fixture: ComponentFixture<MealOfTheDayAdmin>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MealOfTheDayAdmin],
    }).compileComponents();

    fixture = TestBed.createComponent(MealOfTheDayAdmin);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
