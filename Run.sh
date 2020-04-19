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


clear
set -e
trap error SIGHUP

function error()
{
	
    kill $API_ID
    kill $WEB_ID
    
	exit 1
}
