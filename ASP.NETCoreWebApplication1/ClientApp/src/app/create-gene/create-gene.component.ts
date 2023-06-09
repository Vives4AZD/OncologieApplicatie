import { Component } from '@angular/core';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-create-gene',
  templateUrl: './create-gene.component.html',
  styleUrls: ['./create-gene.component.css']
})
export class CreateGeneComponent {
  geneName: string = '';
  cDNA: string = '';

  constructor(private sharedService: SharedService) { }

  createGene() {
    const data = {
      Gene: this.geneName,
      cDNA: this.cDNA
    };

    this.sharedService.CreateGene(data).subscribe(
      (response) => {
        console.log(response);
        // Handle success (e.g., show a success message, redirect to gene list page)
      },
      (error) => {
        console.error(error);
        // Handle error (e.g., show an error message)
      }
    );
  }
}
