#!/bin/bash -e

RED='\033[0;31m'
NC='\033[0m' # No Color

cd ../..  # to reach Dockerfile

docker build \
--tag cart-api:${IMAGE_TAG:-1} \
-f Dockerfile .

EXIT_CODE=$?

if [ $EXIT_CODE -ne 0 ]; then
    echo -e "${RED}Some tests failed!${NC}"
    exit $EXIT_CODE
fi