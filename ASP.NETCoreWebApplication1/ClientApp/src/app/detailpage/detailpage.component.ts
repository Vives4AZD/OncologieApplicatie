import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-detailpage',
  templateUrl: './detailpage.component.html',
  styleUrls: ['./detailpage.component.css']
})
export class DetailpageComponent {
  constructor(private router: Router) {

  }

  NavigateBack() {
    this.router.navigateByUrl('');
  }
}
