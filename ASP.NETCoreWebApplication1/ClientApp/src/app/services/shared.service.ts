import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";


@Injectable({
  providedIn: 'root'
})
export class SharedService{

  constructor( private http: HttpClient) {
  }

  //Get-methods
  GetAllGenes() {return this.http.get("/Gene/GetAllGenes")};
  GetGeneByGuid(id: any) {return this.http.get("/Gene/GetGeneByGuid/" + id)};

  //post-methods
  CreateGenePosition(genePosition: any) {return this.http.post("/Gene/CreateGenePosition" , genePosition )}

  //put-methods
  UpdateGenePosition(genePosition:any, id:any) {return this.http.post("/Gene/UpdateGenePosition/" + id , genePosition)}
}
