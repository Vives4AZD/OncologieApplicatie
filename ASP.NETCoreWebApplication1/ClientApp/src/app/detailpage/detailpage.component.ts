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
  OriginValue: any;

  KeyToUpdate: any;
  ValuetoUpdate: any;

  ShowUpdateModal: boolean = false;
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
  OpenModalUpdate(key: any, value: any) {
    this.ShowUpdateModal = true;

    this.OriginKey = key;
    this.OriginValue = value;

    this.KeyToUpdate = key;
    this.ValuetoUpdate = value;
  }
  UpdateGenePosition() {
    console.log(this.gen.docs[0]);
    for (let key in this.gen.docs[0]) {
      if (key == this.OriginKey) {
        // Update the value of the key to the value of the 'ValuetoUpdate' property
        this.gen.docs[0][key] = this.ValuetoUpdate;
      }
    }
    this.ShowUpdateModal = false;
    console.log(this.gen.docs[0]);
    console.log(this.gen);
    this.ss.UpdateGenePosition(this.gen.docs[0], this.genId).subscribe(d => {
    });
  }

  AddImage(event: any) {
    const file = event.target.files[0];

    // Validate file type
    const allowedTypes = ['image/jpeg', 'image/png', 'image/gif'];
    if (!allowedTypes.includes(file.type)) {
      console.log('Invalid file type. Only JPEG, PNG, and GIF images are allowed.');
      return;
    }

    const reader = new FileReader();

    reader.onload = (e: any) => {
      const base64Image = e.target.result;

      if (!this.gen.docs[0].images) {
        this.gen.docs[0].images = [];
      }

      const newImage = {
        id: this.gen.docs[0].images.length,
        title: file.name,
        src: base64Image
      };
      this.gen.docs[0].images.push(newImage);
      this.ss.UpdateGenePosition(this.gen.docs[0], this.genId).subscribe(d => {
        console.log('Image added successfully');
      });
    };

    reader.readAsDataURL(file);
  }


  DeleteImage(imageId: number) {
    if (!this.gen.docs[0].images) {
      return;
    }

    const imageIndex = this.gen.docs[0].images.findIndex((image: any) => image.id === imageId);
    if (imageIndex > -1) {
      this.gen.docs[0].images.splice(imageIndex, 1);
      this.ss.UpdateGenePosition(this.gen.docs[0], this.genId).subscribe(d => {
        console.log('Image deleted successfully');
      });
    }
  }

}
