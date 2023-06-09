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
  filteredgens:any;
  isSearchEmpty: boolean = false;
  searchText="";
  searchPosition = "";
  constructor(private ss: SharedService, private router: Router) {
  }
  ngOnInit(): void {
    this.ss.GetAllGenes().subscribe( d =>{
      this.gens = d;
    });
  }


  GoToDetail(id: any) {
    this.router.navigateByUrl('/detail/' + id);
  }

  searchKey(data:string)
  {
    if (data == ""){
      this.isSearchEmpty = false;
      this.filteredgens = null;
      return;
    }
    this.isSearchEmpty = true;
    this.searchText=data;
    this.search();
  }
  srcPos(data:string)
  {
    this.search();
    if (data == ""){
      this.filteredgens = null;
      this.searchKey(this.searchText);
      return;
    }
    this.searchPosition = data;
    this.filterPosition();
  }
  search()
  {
    console.log(this.searchText.toLowerCase().trim());
    this.filteredgens = this.gens.rows.filter((t: { doc: { Gene: string; }; }) => t.doc.Gene.toLowerCase().trim().includes(this.searchText.toLowerCase().trim()));
  }

  filterPosition(){
    console.log(this.searchPosition.toLowerCase().trim());
    this.filteredgens = this.filteredgens.filter((p: { doc: { cDNA: string; }; }) => p.doc.cDNA.toLowerCase().trim().includes(this.searchPosition.toLowerCase().trim()));
    console.log(this.filteredgens);
  }

  protected readonly String = String;
}

