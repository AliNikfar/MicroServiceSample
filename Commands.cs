using System;

public class Class1
{
	public Class1()
	{
		//use for docker Update files
		//1- add Container Orchestrator to the project  then select Linux option
		//2- Update Docker-compose.yml and docker-compose.override.yml config files and add configurations
		//3- run the following command to run updated Compose file to project
		// docker-compose -f .\docker-compose.yml -f docker-compose.override.yml up -d

		//to Remove current done docker-compose to project use down intead of up in the command


		//after add ***.Proto file
		//after declare all config and declare class shuold goto this file "Properties" and under "Build Action" select "Protobuf Compiler"
		//then under grpc sturb class select server or client or bilateral
	}
}
