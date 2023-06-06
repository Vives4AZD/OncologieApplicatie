import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  gens: any = [];
  ngOnInit(): void {
    for (let i = 0; i < 100; i++){
      this.gens.push({
        "id": i,
        "Name": 'Gen' + i,
        "Gencode": 'BE12345654'
      });
      console.log(this.gens);
    }
  }
}

