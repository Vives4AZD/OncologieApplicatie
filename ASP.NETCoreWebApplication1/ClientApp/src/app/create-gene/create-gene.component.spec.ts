import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateGeneComponent } from './create-gene.component';

describe('CreateGeneComponent', () => {
  let component: CreateGeneComponent;
  let fixture: ComponentFixture<CreateGeneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateGeneComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateGeneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
