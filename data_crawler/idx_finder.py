import json
import re
import requests

from datetime import date

assembly_data = json.load(open('assembly.json', encoding='utf-8'))
assembly_idx_regex = re.compile(r'dept_cd=([0-9]+)')
open_assembly_idx_regex = re.compile(r'member_seq=([0-9]+)')


def find_popong_idx(name, birthday):
    url = 'http://api.popong.com/v0.1/person/search?q=%s&api_key=test' % name
    birthday_str = birthday.strftime('%Y-%m-%d')
    result = json.loads(requests.get(url).text)
    items = result['items']
    if len(items) == 1:
        return items[0]['id']
    return next((x['id'] for x in items if x['birthday'] == birthday_str), None)


def find_assembly_idx(name, birthday):
    birthday_str = birthday.strftime('%Y-%m-%d')
    item = next((x for x in assembly_data if x[
                'name_kr'] == name and x['birth'] == birthday_str), None)
    if item is None:
        return None
    return int(assembly_idx_regex.search(item['url']).group(1))


def find_open_assembly_idx(name, birthday):
    url = 'http://watch.peoplepower21.org/New/search.php'
    form_data = {'mname': name}  # birthday isn't used yet
    result = requests.post(url, data=form_data).text
    return int(open_assembly_idx_regex.search(result).group(1))

if __name__ == '__main__':
    print(find_assembly_idx('신동우', date(1953, 6, 6)))
    print(find_popong_idx('신동우', date(1953, 6, 6)))
    print(find_open_assembly_idx('신동우', date(1953, 6, 6)))
