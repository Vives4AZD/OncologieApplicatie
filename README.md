
![Logo](https://www.azdelta.be/sites/all/themes/azdelta/images/lgo_azDelta.png)


# OncologieApplicatie

OncologieApplicatie is a NoSQL-based application that allows efficient searching of genetic data stored in a CouchDB database with an Angular UI and .NET based API calls. 
This project aims to provide a fast and scalable solution for researchers and scientists to access and search genetic information.



## Packages
- CouchDB
- .NET Core SDK (version 7.X or higher)
- HttpClient library (e.g., System.Net.Http) 
- Angular
- Microsoft AspNetCore SpaProxy (7.0.5)
## Features

- Search Registred Genes
- See details of Gene
- Angular interface
- Linux(Fedora) executable


## Technological Stack

**Client:** Angular

**Server:** .NET

**Database:** CouchDB



## Installation (Fedora)

1.Install CouchDB with these instructions: https://github.com/adrienverge/copr-couchdb

3.Install official .NET SDK and runtime

```bash
sudo dnf install dotnet-sdk-7.0
```

```bash
sudo dnf install aspnetcore-runtime-7.0
```

4.Get the project from the git

```bash
  git clone https://github.com/Vives4AZD/OncologieApplicatie.git 
  cd my-project
```

5.Go into the project and run it with .NET

```bash
  cd OncologieApplicatie/ASP.NETCoreWebApplication1
  dotnet run
```


    
## Roadmap

- Add Genes

- Adjust Genes details



## Authors

- [@Robbe De Wolf](https://github.com/RobbeDeWolf)
- [@Ian Dumalin](https://github.com/iandumalinvives)
- [@Trúc Vô Thanh](https://github.com/vthanhtruc)
- [@Imran Basit](https://github.com/ImranBasit)

## License

[MIT](https://choosealicense.com/licenses/mit/)
