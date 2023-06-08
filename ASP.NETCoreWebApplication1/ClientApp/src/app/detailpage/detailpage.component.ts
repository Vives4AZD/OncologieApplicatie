import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-detailpage',
  templateUrl: './detailpage.component.html',
  styleUrls: ['./detailpage.component.css']
})
export class DetailpageComponent {
  gen: any;
  genId: any;
  constructor(private ss: SharedService, private router: Router, private route: ActivatedRoute) {
  }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.genId = params.id;
    });
    this.ss.GetGeneByGuid(this.genId).subscribe(d => {
      this.gen = d;
      console.log(this.gen);
    });
  }

  NavigateBack() {
    this.router.navigateByUrl('');
  }
  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
}
