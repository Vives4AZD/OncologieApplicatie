import {Component} from '@angular/core';
import {SharedService} from "../services/shared.service";
import {Router} from '@angular/router'
import {filter} from "rxjs";
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  gens:any;
  filteredgens:any;
  searchText="";
  constructor(private ss: SharedService, private router: Router) {
  }
  ngOnInit(): void {
    this.ss.GetAllGenes().subscribe( d =>{
      this.gens = d;
    });
    console.log(this.gens);
  }


  GoToDetail(id: any) {
    this.router.navigateByUrl('/detail')
  }

  searchKey(data:string)
  {
    if (data == ""){
      this.filteredgens = null;
      return;
    }
    this.searchText=data;
    console.log("searchkeys")
    this.search();
  }

  search()
  {
    console.log(this.searchText.toLowerCase().trim());
    this.filteredgens = this.gens.rows.filter((t: { doc: { Gene: string; }; }) => t.doc.Gene.toLowerCase().trim().includes(this.searchText.toLowerCase().trim()));

  }

  protected readonly filter = filter;
}

