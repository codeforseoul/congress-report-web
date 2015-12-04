# -*- coding: utf-8 -*-
import json
import re

district_data = json.load(open('election-data.json', 'r', encoding='utf-8'))
assembly_data = json.load(open('modified_assembly.json', 'r', encoding='utf-8'))
p_find_idx = re.compile(r'dept_cd=(\d+)')

district_prefix_pair = {
    '전라남도': '전남',
    '경상남도': '경남',
    '부산광역시': '부산',
    '서울특별시': '서울',
    '경기도': '경기',
    '강원도': '강원',
    '인천광역시': '인천',
    '세종특별자치시': None,
    '광주광역시': '광주',
    '대전광역시': '대전',
    '경상북도': '경북',
    '전라북도': '전북',
    '제주특별자치도': '제주',
    '충청남도': '충남',
    '울산광역시': '울산',
    '충청북도': '충북',
    '대구광역시': '대구'
}

def find_cities():
  return list(x.get('name') for x in district_data)

def find_locals(city_name):
  city_info = list(filter(lambda x: x['name'] == city_name, district_data))[0]
  return city_info["district_info"]

def find_towns(city_name, local_name):
  city_info = list(filter(lambda x: x['name'] == city_name, district_data))[0]
  district_info = list(filter(lambda x: x['local'] in local_name, city_info["district_info"]))
  towns = list()
  for district in district_info:
    towns = towns + district['associated_towns']
  return towns

def find_district_name(city_name, local_name, town_name):
  city_info = list(filter(lambda x: x['name'] in city_name, district_data))[0]
  district_info = city_info['district_info']
  district_candidates = list(filter(lambda x: x['local'] == local_name, district_info))
  district_name = list()
  for district_info in district_candidates:
    if town_name in district_info['associated_towns']:
      district_name = district_info['name']
  return district_name

def find_member_info(city_name, local_name, town_name):
    district_name = find_district_name(city_name, local_name, town_name)

    district_prefix = district_prefix_pair[city_name]
    gen_district = city_name
    if district_prefix is not None:
        gen_district = '%s %s' % (district_prefix, district_name)

    return next((x for x in assembly_data if x['district'] == gen_district), None)

if __name__ == '__main__':
    # Test
    member_name = find_member_idx('울산광역시', '울주군', '청량면')
