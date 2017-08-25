# _Band Tracker_

#### _A band tracking website, August 25, 2017_

#### By _**Robert Murray**_

## Description

_A website that stores the information of bands and the venues they have played at. Each venue should show all the bands that have played there, while each band should show all the venues they have played at._

## Setup/Installation Requirements

* _Clone repository_
* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Open MAMP port connection to 8889_

* _In MySQL:_

  **>** CREATE DATABASE band_tracker;
<br>
**>** USE band_tracker;
<br>
**>** CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255));
<br>
**>** CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(255));
<br>
**>** CREATE TABLE bands_venues (id serial PRIMARY KEY, venue_id int, band_id int);


## Technologies Used
* _C#_
* _.NET_
* _[Bootstrap](http://getbootstrap.com/getting-started/)_
* _[mySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **_Robert Murray_**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*
