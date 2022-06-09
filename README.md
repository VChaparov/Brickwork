# Brickwork
Логическо задание
Конзолно приложение, което при дадена площ с размери М и N и попълнена с "тухли" с размер 2x1 да генерира втори слой площ
така че да няма тухли които да се презастъпват напълно. Първоначално се въвежда размера на площта с две четни числа разделени със space(" ").
След това се въвежда ред по ред конфигурацията на тухли. При валидна конфигурация кода генерира втория слой така че да не се препокриват напълно
тухлите с тези от първия слой.

Пример:
Enter two even numbers between 2 and 100 for Layout dimensions N and M
(въвеждате )            2 4
(въвеждате първия ред)  1 1 2 2
(въвеждате втория ред)  3 3 4 4

Ще бъде визуализирано как изглеждат тухлите и ще бъде генериран нов слой който ще завърти тухлите така че да няма пълно презастъпване с долния слой.
Най-много едно от числата от двата чифта числа ще се презастъпва със същото число от горния слой

Резултат:               1 2 3 4
                        1 2 3 4

За улеснение може да се тества като копирате някой от следните редове и натиснете enter:

6 8
1 2 2 3 4 4 5 6
1 7 8 3 9 9 5 6
10 7 8 11 11 12 13 13
10 14 14 15 16 12 17 17
18 19 20 15 16 21 21 22
18 19 20 23 23 24 24 22

10 10
1 2 2 3 4 4 5 6 6 7
1 8 9 3 10 10 5 11 11 7
12 8 9 13 13 14 15 15 16 17
12 18 18 19 20 14 21 21 16 17
22 23 24 19 20 25 25 26 27 27
22 23 24 28 28 29 30 26 31 31
32 33 33 34 35 29 30 36 37 38
32 39 40 34 35 41 41 36 37 38
42 39 40 43 44 44 45 46 46 47
42 48 48 43 49 49 45 50 50 47

При много големи площи поради размера на прозореца на конзолното приложение визуалната структура не се вижда ясно.