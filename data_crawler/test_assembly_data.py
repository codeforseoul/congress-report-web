import json

district_data = json.load(open('election_data.json', encoding='utf-8'))
assembly_data = json.load(
    open('modified_assembly.json', encoding='utf-8'))

cities = []
for x in district_data:
    nm = x['name']
    city_name = nm[0:2]
    if len(nm) == 4:
        city_name = '%s%s' % (nm[0], nm[2])
    print('\'%s\': \'%s\'' % (nm, city_name))
    cities.append(city_name)

for x in assembly_data:
    d = x['district']
    if len(d) == 4:
        print(d)
        continue
    d_city = d.split(' ')[0]
    if d_city not in cities:
        print(d_city + ' not in cities')
        print(repr(x))