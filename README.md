
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
- See and update details of Gene
- Add images
- Angular interface
- Linux(Fedora) executable


## Technological Stack

**Client:** Angular

**Server:** .NET

**Database:** CouchDB



## Installation (Fedora)

### 1. Clone the repository to the desired location

```bash
git clone https://github.com/Vives4AZD/OncologieApplicatie.git /path/desired-location
```

### 3. Install CouchDB

```bash
sudo dnf copr enable adrienverge/couchdb
sudo dnf install couchdb
```

### 4. Configure CouchDB
In the repo there is file 'local.ini' in the directory 'etc'. 
Copy the contents of it to the the CouchDB configuration.
This will make sure that you have a user called 'admin' with password 'admin123'

```bash
sudo cp /path/desired-location/etc/local.ini /etc/couchdb/local.ini
```

### 5. Start CouchDB-service

Use the following command to start CouchDB. The port to access it with the browser (or commandline) is port 5984.

```bash
  sudo systemctl enable --now couchdb.service
```

### 6. Create an initial database (two alternative ways)

#### Option A: Visual: With Couchdb-webinterface
- Go to http://localhost:5984/oncologie/_utils
- Log in with username 'admin' and password 'admin123'
- Click on the button 'Create Database' name it 'oncologie' and create

#### Option B Commandline

Use curl and use the following bash command. You need to give in the 'user' and 'password'. You will get a confirmation if it succeeded or not.
```bash
curl -X PUT http://localhost:5984/oncologie -u admin:admin123
```

### 7. Create a new entry/document (two alternative ways)
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

```bash
sudo dnf install dotnet-sdk-7.0
```

Install Node.json
```bash
sudo dnf install nodejs
```

### 8. Running the WebApplication

Go to the location where you pulled the repo in and execute it with dotnet

```bash
cd /path/desired-location/ASP.NETCoreWebApplication1

dotnet run
```

The application will now go up and running.

You will get a port that you will use to reach the application.


https://localhost:7255

https://localhost:44407



### 9. Ending the application

To end the WebApplication, you need to make sure the ports are all closed.

To make sure, run this command and enter the port number you used to reach the application.

```bash
sudo lsof -i :44407
```

If they are still open. Perform the underlying command but replace 'pid' with the actual pid number you saw with the before mentioned command

```bash
sudo kill -9 <pid>
```

The application should be fully closed now and be able to run once more if desired.
## Roadmap

- Add Genes



## Authors

- [@Robbe De Wolf](https://github.com/RobbeDeWolf)
- [@Ian Dumalin](https://github.com/iandumalinvives)
- [@Trúc Vô Thanh](https://github.com/vthanhtruc)
- [@Imran Basit](https://github.com/ImranBasit)

## License

[MIT](https://choosealicense.com/licenses/mit/)
