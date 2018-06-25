#include "stdafx.h"
#include <iostream>
using namespace std;

class A {
public:
	void podoperacja() {
		std::cout << "metoda A" << std::endl;
	}
};

class B {
public:
	void podoperacja() {
		std::cout << "metoda B" << std::endl;
	}
};


class Fasada {
public:
	Fasada() :
		subsystemA(), subsystemB() {}

	void operacja_1() {
		subsystemA->podoperacja();
		subsystemB->podoperacja();
	}

	void operacja_2() {
		subsystemB->podoperacja();
	}

private:
	A *subsystemA;
	B *subsystemB;
};


int main()
{
	Fasada *ladnaFasada = new Fasada();
	ladnaFasada->operacja_1();
	ladnaFasada->operacja_2();
	system("pause");
	return 0;
}
