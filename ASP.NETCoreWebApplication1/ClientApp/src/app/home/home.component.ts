import { Component } from '@angular/core';
import { SharedService } from "../services/shared.service";
import { Router } from '@angular/router'
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  gens: any;
  filteredgens: any;
  isSearchEmpty: boolean = false;
  searchText = "";
  searchPosition = "";

  createdGenePosition: any;

  GenNames: any = [];
  NewGenposition: any = {
    'Gene': "",
    'cDNA': "",
    'Protein': "",
    'Tier': ""
  };

  showModalAddGenePosition: boolean = false;
  constructor(private ss: SharedService, private router: Router) {
  }
  ngOnInit(): void {
    this.RefreshList();
  }


  GoToDetail(id: any) {
    this.router.navigateByUrl('/detail/' + id);
  }

  GetUniqueGeneName() {
    this.GenNames.push(this.gens.rows[0].doc.Gene);
    for (let gen of this.gens.rows) {
      if (!this.GenNames.includes(gen.doc.Gene)) {
        this.GenNames.push(gen.doc.Gene);
      }
    }
    console.log(this.GenNames);
  }
  searchKey(data: string) {
    if (data == "") {
      this.isSearchEmpty = false;
      this.filteredgens = null;
      return;
    }
    this.isSearchEmpty = true;
    this.searchText = data;
    this.search();
  }
  srcPos(data: string) {
    this.search();
    if (data == "") {
      this.filteredgens = null;
      this.searchKey(this.searchText);
      return;
    }
    this.searchPosition = data;
    this.filterPosition();
  }
  search() {
    console.log(this.searchText.toLowerCase().trim());
    this.filteredgens = this.gens.rows.filter((t: { doc: { Gene: string; }; }) => t.doc.Gene.toLowerCase().trim().includes(this.searchText.toLowerCase().trim()));
  }

  filterPosition() {
    console.log(this.searchPosition.toLowerCase().trim());
    this.filteredgens = this.filteredgens.filter((p: { doc: { cDNA: string; }; }) => p.doc.cDNA.toLowerCase().trim().includes(this.searchPosition.toLowerCase().trim()));
    console.log(this.filteredgens);
  }
  clearModal() {
    this.NewGenposition = {
      'Gene': "",
      'cDNA': "",
      'Protein': "",
      'Tier': ""
    }
    this.showModalAddGenePosition = false;
  }

  AddGeneposition() {
    this.ss.CreateGenePosition(this.NewGenposition).subscribe(d => {

      this.createdGenePosition = d;
      console.log(d);
      this.RefreshList();
      this.showModalAddGenePosition = false;
    });
  }
  RefreshList() {
    this.ss.GetAllGenes().subscribe(d => {
      this.gens = d;
      this.GetUniqueGeneName();
    });
  }

  protected readonly String = String;
}

