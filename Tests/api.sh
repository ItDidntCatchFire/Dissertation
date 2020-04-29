#!/bin/bash
Expected=""
#Port=5007
#IP=$(ip addr show wlp36s0 | grep -Po 'inet \K[\d.]+')
Authentiction="0f8fad5b-d9cb-469f-a165-70867728950e"
Port=5001
IP="localhost"
 
#Start the process
cd ../API
#nohup dotnet run --urls "https://${IP}:${Port}" > /dev/null 2>&1 &
#sleep 5

cd ../Tests
host=$"https://${IP}:${Port}/api/"
printf $"Host ${host}\n"
rm "results.txt"

set -e
trap error SIGHUP

function error()
{
	if [[ $Expected != $var ]]
    then
        printf "Expected\n\t"
        printf '%s\n' "${Expected}"
        printf "\n"
    fi
	printf "What actually came back\n\t"
	cat results.txt
	exit 1
}

printf "Items\n"
printf "\tInsert\n"
printf "\t\t\tNo Authentiction\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json') == 500 ]]
then 
	 var=$(<results.txt)
	 Expected='"Failure"'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction'h' -H 'Content-Type: application/json') == 500 ]]
then 
	 var=$(<results.txt)
	 Expected='"Failure"'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tName\n"
printf "\t\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","Name":null,"Description":"description","ShelfLife":0,"BuyPrice":0.0,"SellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tBLANK\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tSPACE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"  ","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tDescription\n"
printf "\t\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":null,"shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Description missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tBLANK\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Description missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tSPACE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":" ","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Description missing"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tShelfLife\n"
printf "\t\t\tNEGATIVE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"description","shelfLife":-1,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["ShelfLife negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tBuyPrice\n"
printf "\t\t\tNEGATIVE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"description","shelfLife":0,"buyPrice":-1.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["BuyPrice negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tSellPrice\n"
printf "\t\t\tNEGATIVE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":-1.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["SellPrice negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tAll Broken\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"","description":"","shelfLife":-1,"buyPrice":-1.0,"sellPrice":-1.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing","Description missing","ShelfLife negative","BuyPrice negative","SellPrice negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tAll Fixed\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"TestName","description":"TestDescrption","shelfLife":45,"buyPrice":10.50,"sellPrice":13.05}') == 200 ]]
then 
	 var=$(<results.txt)
     Expected="TestName"    
     if [[ "$var" != *"$Expected"* ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\tUpdate\n"
printf "\t\t\tNo Authentiction\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json') == 500 ]]
then 
	 var=$(<results.txt)
     Expected='"Failure"'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction'h' -H 'Content-Type: application/json') == 500 ]]
then 
	 var=$(<results.txt)
     Expected='"Failure"'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;


printf "\t\tName\n"
printf "\t\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","Name":null,"Description":"description","ShelfLife":0,"BuyPrice":0.0,"SellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tBLANK\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tSPACE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"  ","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tDescription\n"
printf "\t\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"name","description":null,"shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Description missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tBLANK\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"name","description":"","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Description missing"]'
     if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tSPACE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"name","description":" ","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Description missing"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tShelfLife\n"
printf "\t\t\tNEGATIVE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"name","description":"description","shelfLife":-1,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["ShelfLife negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tBuyPrice\n"
printf "\t\t\tNEGATIVE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"name","description":"description","shelfLife":0,"buyPrice":-1.0,"sellPrice":0.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["BuyPrice negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tSellPrice\n"
printf "\t\t\tNEGATIVE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"name","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":-1.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["SellPrice negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tAll Broken\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"","description":"","shelfLife":-1,"buyPrice":-1.0,"sellPrice":-1.0}') == 400 ]]
then 
	 var=$(<results.txt)
     Expected='["Name missing","Description missing","ShelfLife negative","BuyPrice negative","SellPrice negative"]'
    if [[ $Expected != "$var" ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tAll Fixed\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '{"itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","name":"TestName","description":"TestDescrption","shelfLife":45,"buyPrice":10.50,"sellPrice":13.05}') == 200 ]]
then 
	 var=$(<results.txt)
     Expected="TestName"    
     if [[ "$var" != *"$Expected"* ]]
     then
         printf "Failed \n"
         kill -1 $$
     fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\tGet\n"
printf "\t\tEMPTY Guid\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/00000000-0000-0000-0000-000000000000) == 400 ]]
then 
    var=$(<results.txt)
    Expected="Bad Request"    
    if [[ "$var" != *"$Expected"* ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tEMPTY\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/) == 500 ]]
then 
    var=$(<results.txt)
    Expected='Failure'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tSPACE\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/ ) == 500 ]]
then 
    var=$(<results.txt)
    Expected='Failure'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tUNKNOWN\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/1f8fad5b-d9cb-469f-a165-70867728950e) == 500 ]]
then 
    var=$(<results.txt)
    Expected="Failure"    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tKNOWN\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/0f8fad5b-d9cb-469f-a165-70867728950e) == 200 ]]
then 
    var=$(<results.txt)
    Expected="0f8fad5b-d9cb-469f-a165-70867728950e"    
    if [[ "$var" != *"$Expected"* ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;



printf "\tList\n"
printf "\t\tNo Authentiction\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/List) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/List -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\t\tWorking\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/List -H 'ID: '$Authentiction) != 200 ]]
then 
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "User\n"
printf "\tPostUser\n"
printf "\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}User) != 415 ]]
then 
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tBLANK\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}User -H 'Content-Type: application/json') != 400 ]]
then 
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tSPACE\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}User -H 'Content-Type: application/json' -d " ") != 400 ]]
then 
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tInvalid\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}User -H 'Content-Type: application/json' -d '"eaa0ec62-7e0d-454c-966a-171cbb17b0a1"') != 500 ]]
then 
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\tWorking\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}User -H 'Content-Type: application/json' -d '"0f8fad5b-d9cb-469f-a165-70867728950e"') != 200 ]]
then 
    var=$(<results.txt)
    Expected="0f8fad5b-d9cb-469f-a165-70867728950e"    
    if [[ "$var" != *"$Expected"* ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;

    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "Inventory\n"
printf "\tList\n"
printf "\t\t\tNo Authentication\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory/List) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\t\t\tWorking\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction) != 200 ]]
then 
    printf "  http code Fail\n"
    kill -1 $$
fi;

printf "\tInsert\n"
printf "\t\t\tNo Authentication\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\t\t\tItemId\n"
printf "\t\t\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["ItemId Empty"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\t\tEmpty\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"00000000-0000-0000-0000-000000000000","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["ItemId Empty"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tQuantity\n"
printf "\t\t\t\tNegative\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":-1,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["Quantity negative"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\t\tZero\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":0,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["Quantity zero"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tMonies\n"
printf "\t\t\t\tNegative\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":-1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["Monies negative"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tAll Broken\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"00000000-0000-0000-0000-000000000000","quantity":0,"time":"2020-01-01T00:00:00","export":false,"monies":-1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["ItemId Empty","Quantity zero","Monies negative"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tAll Fixed\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":10.0}]') == 200 ]]
then 
    var=$(<results.txt)
    Expected="0f8fad5b-d9cb-469f-a165-70867728950e"    
    if [[ "$var" != *"$Expected"* ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\tInsert List\n"
printf "\t\t\tNo Authentication\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\t\t\tItemId\n"
printf "\t\t\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["ItemId Empty"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\t\tEmpty\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"00000000-0000-0000-0000-000000000000","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["ItemId Empty"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tQuantity\n"
printf "\t\t\t\tNegative\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":-1,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["Quantity negative"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\t\tZero\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":0,"time":"2020-01-01T00:00:00","export":false,"monies":1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["Quantity zero"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tMonies\n"
printf "\t\t\t\tNegative\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":-1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["Monies negative"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tAll Broken\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"00000000-0000-0000-0000-000000000000","quantity":0,"time":"2020-01-01T00:00:00","export":false,"monies":-1.0}]') == 400 ]]
then 
    var=$(<results.txt)
    Expected='["ItemId Empty","Quantity zero","Monies negative"]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\t\t\tAll Fixed\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Inventory/List -H 'ID: '$Authentiction -H 'Content-Type: application/json' -d '[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":1,"time":"2020-01-01T00:00:00","export":false,"monies":10.0}]') == 200 ]]
then 
    var=$(<results.txt)
    Expected="0f8fad5b-d9cb-469f-a165-70867728950e"    
    if [[ "$var" != *"$Expected"* ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;  
else
    printf "  http code Fail\n"
	kill -1 $$
fi;

printf "\tGet\n"
printf "\t\t\tNo Authentication\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\t\t\tWorking\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory/5b078b5a-d987-4424-88ea-57f2cca2866e -H 'ID: '$Authentiction) != 200 ]]
then 
    printf "  http code Fail\n"
  	kill -1 $$
fi;

printf "\tStock\n"
printf "\t\t\tNo Authentication\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory/Stock) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory/Stock -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\t\t\tWorking\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Inventory/Stock -H 'ID: '$Authentiction) == 200 ]]
then 
    var=$(<results.txt)
    Expected='[{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"0f8fad5b-d9cb-469f-a165-70867728950e","quantity":-8,"time":"0001-01-01T00:00:00","export":false,"monies":0},{"inventoryId":"00000000-0000-0000-0000-000000000000","itemId":"eaa0ec62-7e0d-454c-966a-171cbb17b0a1","quantity":-1,"time":"0001-01-01T00:00:00","export":false,"monies":0}]'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "Transaction\n"
printf "\tCost\n"
printf "\t\t\tNo Authentication\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Transaction/Cost) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Transaction/Cost -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

dateTo=(date +%FT%T)
printf "\t\t\tWorking\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Transaction/Cost?dateFrom=0001-01-01T00:00:00\&dateTo=${dateTo} -H 'ID: '$Authentiction) == 200 ]]
then 
    var=$(<results.txt)
    Expected='0'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\tRevenue\n"
printf "\t\t\tNo Authentication\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Transaction/Revenue) != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
		kill -1 $$
    fi;  
fi;

printf "\t\t\tUnauthenticated\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Transaction/Revenue -H 'ID: '$Authentiction'h') != 500 ]]
then 
    var=$(<results.txt)
    Expected='"Unauthorized"'    
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;

printf "\t\t\tWorking\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Transaction/Revenue?dateFrom=0001-01-01T00:00:00\&dateTo=${dateTo} -H 'ID: '$Authentiction) == 200 ]]
then 
    var=$(<results.txt)
    Expected='4'
    if [[ $Expected != "$var" ]]
    then
        printf "Failed \n"
        kill -1 $$
    fi;
fi;
printf "Passed\n"