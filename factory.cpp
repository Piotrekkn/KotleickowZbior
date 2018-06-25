#include "stdafx.h"
#include <iostream>
using namespace std;

template <class KlasaT>
class TPamiatka
{
	KlasaT stan;
public:
	TPamiatka(const KlasaT & stan) : stan(stan)
	{
	}
	const KlasaT & odczytajStan() const
	{
		return stan;
	}
};

class CObiektProgramu
{
private:
	double dlugosc;
	int ciezar;
	string nazwa;

public:
	TPamiatka <CObiektProgramu> *zapiszStan() const
	{
		return new TPamiatka <CObiektProgramu>(*this);
	}

	void wczytajStan(const TPamiatka < CObiektProgramu >& memento)
	{
		const CObiektProgramu & stan = memento.odczytajStan();
		dlugosc = stan.dlugosc;
		ciezar = stan.ciezar;
		nazwa = stan.nazwa;
	}
};

int main()
{
	cout << "Memento" << endl;

	system("pause");
	return 0;
}
