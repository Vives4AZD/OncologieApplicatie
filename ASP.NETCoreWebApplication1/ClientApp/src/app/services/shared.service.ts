import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";


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
  CreateGenePosition(genePosition: any) {return this.http.post("/Gene/CreateGenePosition" , genePosition )}
}
