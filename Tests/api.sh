#!/bin/bash
Run.sh

#Get IP
rm ip.txt
ip addr show wlp36s0 | grep -Po 'inet \K[\d.]+' >> ip.txt
ip=$(<ip.txt)

host='https://${ip}:5321/api/'


