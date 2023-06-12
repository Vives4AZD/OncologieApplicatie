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

  /**
   * Navigates to the detail page for a given ID.
   *
   * @param {any} id - The ID of the item to show details for.
   * @return {void} - This function does not return anything.
   */
  GoToDetail(id: any) {
    this.router.navigateByUrl('/detail/' + id);
  }

  /**
   * Gets all the Gene values from the database
   *
   * @return {void} - This function does not return anything.
   */
  GetUniqueGeneName() {
    this.GenNames.push(this.gens.rows[0].doc.Gene);
    // Initialize an array with the Gene values from the database.
    for (let gen of this.gens.rows) {
      if (!this.GenNames.includes(gen.doc.Gene)) {
        this.GenNames.push(gen.doc.Gene);
      }
    }
  }

  /**
   * This function searches for a key in the data and updates the state variables accordingly.
   *
   * @param {string} data - the key to search for
   * @return {void} - This function does not return anything.
   */
  searchKey(data: string) {
    // If the search term is empty, update the state variables and return
    if (data == "") {
      this.isSearchEmpty = false;
      this.filteredgens = null;
      return;
    }
    // If the search term is not empty, update the state variables and perform search
    this.isSearchEmpty = true;
    this.searchText = data;
    this.search();
  }

  /**
   * Sets the search position and filters the position list accordingly.
   *
   * @param {string} data - the search position to set
   * @return {void} - This function does not return anything.
   */
  srcPos(data: string) {
    // If the search term is empty, update the state variables and return
    this.search();
    if (data == "") {
      this.filteredgens = null;
      this.searchKey(this.searchText);
      return;
    }
    // If the search term is not empty, update the state variables and perform search
    this.searchPosition = data;
    this.filterPosition();
  }
  
  /**
   * Filters the rows of found genes based on a search value. This is to search for a Gene value.
   *
   * @return {void} - This function does not return anything.
   */
  search() {
    // Applies the Gene search term to the filtered list of genes.
    this.filteredgens = this.gens.rows.filter((t: { doc: { Gene: string; }; }) => t.doc.Gene.toLowerCase().trim().includes(this.searchText.toLowerCase().trim()));
  }

  /**
   * Filters the rows of found genes based on a search value. This is to search for a cDNA value, provided the Gene value is filled in.
   *
   * @return {void} - This function does not return anything.
   */
  filterPosition() {
    // Applies the cDNA search term to the filtered list of genes, only shows up when Gene isn't empty.
    this.filteredgens = this.filteredgens.filter((p: { doc: { cDNA: string; }; }) => p.doc.cDNA.toLowerCase().trim().includes(this.searchPosition.toLowerCase().trim()));
  }
  
  /**
   * Clears the modal.
   *
   * @return {void} - This function does not return anything.
   */
  clearModal() {
    this.NewGenposition = {
      'Gene': "",
      'cDNA': "",
      'Protein': "",
      'Tier': ""
    }
    this.showModalAddGenePosition = false;
  }

  /**
   * Adds a new gene position to the database.
   * 
   * @return {void} - This function does not return anything.
   */
  AddGeneposition() {
    this.ss.CreateGenePosition(this.NewGenposition).subscribe(d => {
      this.createdGenePosition = d;
      this.RefreshList();
      this.showModalAddGenePosition = false;
    });
  }

  /**
   * Refreshes the list of genes.
   * 
   * @return {void} - This function does not return anything.
   */
  RefreshList() {
    this.ss.GetAllGenes().subscribe(d => {
      this.gens = d;
      this.GetUniqueGeneName();
    });
  }

  protected readonly String = String;
}

