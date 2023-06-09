import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-edit-gene',
  templateUrl: './edit-gene.component.html',
  styleUrls: ['./edit-gene.component.css']
})
export class EditGeneComponent implements OnInit {
  geneId: string;
  geneData: any = {}; // Initialize with an empty object

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private sharedService: SharedService
  ) {
    this.geneId = this.route.snapshot.params.id;
  }

  ngOnInit(): void {
    this.sharedService.GetGeneByGuid(this.geneId).subscribe(
      (data) => {
        this.geneData = data;
      },
      (error) => {
        console.error('Failed to retrieve gene data:', error);
      }
    );
  }

  updateGene() {

   
    this.sharedService.UpdateGene(this.geneId, this.geneData).subscribe(
      (data) => {
        console.log('Gene updated successfully:', data);
        this.router.navigateByUrl(`/detail/${this.geneId}`);
      },
      (error) => {
        console.error('Failed to update gene:', error);
        console.log(this.geneId.toString);
      }
    );
  }

 

  cancelEdit() {
    // Redirect to the detail page or any other desired page
    this.router.navigateByUrl(`/detail/${this.geneId}`);
  }

  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
}
