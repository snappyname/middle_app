import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GithubAuthCallback } from './github-auth-callback';

describe('GithubAuthCallback', () => {
  let component: GithubAuthCallback;
  let fixture: ComponentFixture<GithubAuthCallback>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GithubAuthCallback]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GithubAuthCallback);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
