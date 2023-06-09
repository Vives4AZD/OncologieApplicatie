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
      console.log(this.gen);
    });
  }

  NavigateBack() {
    this.router.navigateByUrl('');
  }
  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
  OpenModalUpdate(key: any, value:any){
    this.ShowUpdateModal = true;

    this.OriginKey = key;
    this.OriginValue = value;

    this.KeyToUpdate = key;
    this.ValuetoUpdate = value;
  }
  UpdateGenePosition(){
    console.log(this.gen.docs[0]);
    for (let key in this.gen.docs[0]){
      if (key == this.OriginKey){
        key = this.KeyToUpdate;
      }
    }
    console.log(this.gen.docs[0]);
    console.log(this.gen);
    this.ss.UpdateGenePosition(this.gen.docs[0], this.genId).subscribe(d =>{
    });
  }
}
