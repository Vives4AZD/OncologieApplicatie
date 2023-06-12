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

  OriginKey: any;
  OriginValue:any;

  KeyToUpdate: any;
  ValuetoUpdate: any;

  ShowUpdateModal: boolean= false;
  constructor(private ss: SharedService, private router: Router, private route: ActivatedRoute) {
  }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.genId = params.id;
    });
    this.ss.GetGeneByGuid(this.genId).subscribe(d => {
      this.gen = d;
    });
  }

  /**
   * Navigates back to the home page.
   * 
   * @return {void} - This function does not return anything.
   */
  NavigateBack() {
    this.router.navigateByUrl('');
  }

  /**
   * Gets a list of keys in a provided gene doc.
   * 
   * @param {any} obj - the full gene doc.
   * @returns {string[]} - A list of all the keys in the doc.
   */
  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  /**
   * Opens an update modal.
   * 
   * @param key - the name of the key to update
   * @param value - the value to update
   */
  OpenModalUpdate(key: any, value:any){
    this.ShowUpdateModal = true;

    this.OriginKey = key;
    this.OriginValue = value;

    this.KeyToUpdate = key;
    this.ValuetoUpdate = value;
  }

  /**
   * Updates the value of a key in a gene doc.
   * 
   * @return {void} - This function does not return anything.
   */
  UpdateGenePosition(){
    console.log(this.gen.docs[0]);
    for (let key in this.gen.docs[0]){
      if (key == this.OriginKey){
        this.gen.docs[0][key] = this.ValuetoUpdate;
      }
    }
    this.ss.UpdateGenePosition(this.gen.docs[0], this.genId).subscribe(d =>{
      this.ShowUpdateModal = false;
    });
  }
}
