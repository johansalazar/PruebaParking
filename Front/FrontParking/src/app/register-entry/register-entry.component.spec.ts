import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterEntryComponent } from './register-entry.component';

describe('RegisterEntryComponent', () => {
  let component: RegisterEntryComponent;
  let fixture: ComponentFixture<RegisterEntryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegisterEntryComponent]
    });
    fixture = TestBed.createComponent(RegisterEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
