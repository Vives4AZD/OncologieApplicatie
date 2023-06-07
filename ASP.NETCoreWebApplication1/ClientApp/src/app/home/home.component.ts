import {Component} from '@angular/core';
import {SharedService} from "../services/shared.service";
import {Router} from '@angular/router'
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  gens:any;
  constructor(private ss: SharedService, private router: Router) {
  }
  ngOnInit(): void {
    this.ss.GetAllGenes().subscribe( d =>{
      this.gens = d;
    });
    console.log(this.gens);
  }
  GoToDetail(id: any){
    this.router.navigateByUrl('/detail')
  }
}

