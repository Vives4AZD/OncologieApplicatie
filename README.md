
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
<<<<<<< HEAD
- See and update details of Gene
=======
- See details of Gene
>>>>>>> parent of 4d520e0 (Update README.md)
- Angular interface
- Linux(Fedora) executable
- Add and delete images

## Technological Stack

**Client:** Angular

**Server:** .NET

**Database:** CouchDB



## Installation (Fedora)

1.Install CouchDB with these instructions: https://github.com/adrienverge/copr-couchdb

<<<<<<< HEAD
```bash
git clone https://github.com/Vives4AZD/OncologieApplicatie.git /path/desired-location
```

### 2. Install CouchDB

```bash
sudo dnf copr enable adrienverge/couchdb
sudo dnf install couchdb
```

### 3. Configure CouchDB
In the repo there is file 'local.ini' in the directory 'etc'. 
Copy the contents of it to the the CouchDB configuration.
This will make sure that you have a user called 'admin' with password 'admin123'

```bash
sudo cp /path/desired-location/etc/local.ini /etc/couchdb/local.ini
```

### 4. Start CouchDB-service

Use the following command to start CouchDB. The port to access it with the browser (or commandline) is port 5984.

```bash
  sudo systemctl enable --now couchdb.service
```

### 5. Create an initial database (two alternative ways)

#### Option A: Visual: With Couchdb-webinterface
- Go to http://localhost:5984/oncologie/_utils
- Log in with username 'admin' and password 'admin123'
- Click on the button 'Create Database' name it 'oncologie' and create

#### Option B Commandline

Use curl and use the following bash command. You need to give in the 'user' and 'password'. You will get a confirmation if it succeeded or not.
```bash
curl -X PUT http://localhost:5984/oncologie -u admin:admin123
```

### 6. Create a new entry/document (two alternative ways)
#### Option A: Visuaith l with CouchDb-webinterface

Go to http://localhost:5984/oncologie and next to 'All Documents' you have a plus sign you can click on to add a new entry.

Copy the following content:

```bash
{
  "Gene": "BRAF",
  "ENS": "ENST00000288602",
  "cDNA": "c.1799_1800delTGinsAA",
  "Protein": "p.V600E",
  "Tier": "I",
  "Evidence source": "NCCN",
  "Approved FDA": "",
  "Approved FDA for others": "",
  "NCCN drug": "Cetuximab;panitumumab;vemurafenib",
  "NCCN response": "resistance",
  "NCCN condition": "",
  "NCCN panel": "The panel believes that evidence increasingly suggests that BRAF V600E mutation makes response to panitumumab or cetuximab, as single agents or in combination with cytotoxic chemotherapy, highly unlikely, unless given with a BRAF inhibitor",
  "Investigational Drug": "",
  "Preclinical evidence": "",
  "Preclinical evidence for others": "",
  "Available clinical trials": "",
  "Submitted tissue": "colon",
  "Allele Count": "0"
}
```

It will automatically create an Id and rev variable for it.

#### Option B: Commandline
Use Curl once more and copy paste the following command:

```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "Gene": "BRAF",
  "ENS": "ENST00000288602",
  "cDNA": "c.1799_1800delTGinsAA",
  "Protein": "p.V600E",
  "Tier": "I",
  "Evidence source": "NCCN",
  "Approved FDA": "",
  "Approved FDA for others": "",
  "NCCN drug": "Cetuximab;panitumumab;vemurafenib",
  "NCCN response": "resistance",
  "NCCN condition": "",
  "NCCN panel": "The panel believes that evidence increasingly suggests that BRAF V600E mutation makes response to panitumumab or cetuximab, as single agents or in combination with cytotoxic chemotherapy, highly unlikely, unless given with a BRAF inhibitor",
  "Investigational Drug": "",
  "Preclinical evidence": "",
  "Preclinical evidence for others": "",
  "Available clinical trials": "",
  "Submitted tissue": "colon",
  "Allele Count": "0"
}' -u admin:admin123 http://localhost:5984/oncologie
```

Note: Id and Rev values will get added automatically.

### 7. Setup necessary packages to execute WebInterface application

Install .NET SDK
This package is the one recommended by Microsoft. Install the latest, in this case it's 7.0.
=======
3.Install official .NET SDK and runtime
>>>>>>> parent of 4d520e0 (Update README.md)

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
<<<<<<< HEAD
- Add/Delete Images
=======

- Adjust Genes details

>>>>>>> parent of 4d520e0 (Update README.md)


## Authors

- [@Robbe De Wolf](https://github.com/RobbeDeWolf)
- [@Ian Dumalin](https://github.com/iandumalinvives)
- [@Trúc Vô Thanh](https://github.com/vthanhtruc)
- [@Imran Basit](https://github.com/ImranBasit)

## License

[MIT](https://choosealicense.com/licenses/mit/)
