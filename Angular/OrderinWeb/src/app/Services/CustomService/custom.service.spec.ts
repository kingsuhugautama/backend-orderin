import { TestBed } from '@angular/core/testing';

import { CustomService } from './custom.service';

describe('JqueryService', () => {
  let service: CustomService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
