import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoogleAuthCallback } from './google-auth-callback';

describe('GoogleAuthCallback', () => {
  let component: GoogleAuthCallback;
  let fixture: ComponentFixture<GoogleAuthCallback>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GoogleAuthCallback]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GoogleAuthCallback);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
