import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import * as fs from 'fs';
import { Document } from "./interfaces/Document";

@Injectable({
  providedIn: 'root'
})
export class SharedService{

  constructor( private http: HttpClient) {
  }

  //Getters
  GetAllGenes() {return this.http.get("/Gene/GetAllGenes")};
  GetGeneByGuid(id: any) {return this.http.get("/Gene/GetGeneByGuid/" + id)};
  //Setters

  //Posters
  PostBulk(file: File) {return this.http.get('/Bulkpost')};
}
