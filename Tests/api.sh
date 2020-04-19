#!/bin/bash
Expected=""

cd ../API
nohup dotnet run --urls https://0.0.0.0:5321 > /dev/null 2>&1 &
sleep 5

#Get IP
cd ../Web/WebApplication/wwwroot
rm ip.json
echo "\"https://$(ip addr show wlp36s0 | grep -Po 'inet \K[\d.]+'):5321/\"" >> ip.json
ip=$(<ip.json)

cd ../../../Tests
host=$"${ip:1:-1}api/"
printf $"Host ${host}\n"


set -e
trap error SIGHUP

function error()
{
	if [[ $local == 1 ]] 
	 then
		kill $PROC_ID
	 fi
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



printf "Item\n"
printf "\tInsert\n"
printf "\t\tName\n"
printf "\t\t\tNULL\n"
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","Name":null,"Description":"description","ShelfLife":0,"BuyPrice":0.0,"SellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"  ","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":null,"shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":" ","shelfLife":0,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"description","shelfLife":-1,"buyPrice":0.0,"sellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"description","shelfLife":0,"buyPrice":-1.0,"sellPrice":0.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"name","description":"description","shelfLife":0,"buyPrice":0.0,"sellPrice":-1.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"","description":"","shelfLife":-1,"buyPrice":-1.0,"sellPrice":-1.0}') == 400 ]]
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
if [[ $(curl -s -k -o results.txt -X POST -w '%{http_code}' ${host}Items -H 'Content-Type: application/json' -d '{"itemId":"00000000-0000-0000-0000-000000000000","name":"TestName","description":"TestDescrption","shelfLife":45,"buyPrice":10.50,"sellPrice":13.05}') == 200 ]]
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
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/) == 404 ]]
then 
    var=$(<results.txt)
    Expected=""    
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
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/ ) == 404 ]]
then 
    var=$(<results.txt)
    Expected=""    
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
printf "\t\tGet\n"
if [[ $(curl -s -k -o results.txt -w '%{http_code}' ${host}Items/List) != 200 ]]
then 
    printf "  http code Fail\n"
	kill -1 $$
fi;
