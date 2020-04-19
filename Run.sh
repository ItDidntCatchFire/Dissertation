#Start Api
cd API
dotnet run -v q --urls https://0.0.0.0:5321 > /dev/null &
API_ID=$!
echo $API_ID

#Start the website
cd ../Web/WebApplication
dotnet run -v q --urls https://0.0.0.0:5322 > /dev/null &
WEB_ID=$!
echo $WEB_ID

