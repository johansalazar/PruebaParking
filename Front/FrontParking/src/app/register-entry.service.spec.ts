import { TestBed } from '@angular/core/testing';

import { RegisterEntryService } from './register-entry.service';

describe('RegisterEntryService', () => {
  let service: RegisterEntryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterEntryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
