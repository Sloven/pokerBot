@echo off
bin\proxytrim %1 | bin\sort | bin\uniq > %2
