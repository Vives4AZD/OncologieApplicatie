import {Component, Inject} from '@angular/core';
import {SharedService} from "../services/shared.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']

})
export class HomeComponent {
  gens: any = [];

  constructor(private ss: SharedService) {
  }
  ngOnInit(): void {
    for (let i = 0; i < 100; i++){
      this.gens.push({
        "id": i,
        "Name": 'Gen' + i,
        "Gencode": 'BE12345654'
      });

      this.ss.GetAllGenes().subscribe( d =>{
        this.gens = d;
      });
      console.log(this.gens);
    }
  }
}

