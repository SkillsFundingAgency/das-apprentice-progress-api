# ApprenticeProgress API

The ApprenticeProgress API exposes information relating to apprentice progress items as features in apprentice-app PWA, the data of which is held in its underlying SQL database.


## How It Works

The API is a simple .Net Core restful API with a SQL Server backend.


## 🚀 Installation

### Pre-Requisites

* A clone of this repository
* A code editor that supports .NetCore 6
* A SQL Server installation
* Storage emulator
* Database project published to local db server

### Config

This services uses the standard Apprenticeship Service configuration. All configuration can be found in the [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config/tree/master/das-ApprenticeProgress-api).

Configuration should be added to the local emulated storage using partition key `LOCAL` and RowKey `SFA.DAS.ApprenticeProgress.Api_1.0`, and should resemble the following:

```
{
  "ApprenticeProgress": {
    "ConnectionString": "<local db connection string>"
  },
  "AzureAd": {
    "tenant": "<tenant>",
    "identifier": "<service identifier>"
  }
}
```



## 🔗 External Dependencies

ApprenticeProgress API has no external dependenices.

## Technologies

* .NetCore 8
* Web API
* SQL Server
* Entity Framework
* NLog
* Azure Table Storage
* NUnit
* Moq
* FluentAssertions

